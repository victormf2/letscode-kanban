import { Card, CardListResult } from '@/app/card/card';
import { CardsService } from '@/app/card/cards.service';
import { groupBy } from '@/helpers/list-helpers';
import { Component, OnInit } from '@angular/core';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { finalize } from 'rxjs/operators';
import { CardEvents, CardMoving, CardRemoving } from '@/app/card/card-events';
import { ListConfig } from './list/list-config';
import { ListContext } from './list/list-context';
import { NotificationsService } from '@/app/notifications/notifications.service';

@UntilDestroy()
@Component({
  selector: 'app-board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.scss']
})
export class BoardComponent implements OnInit {

  listConfigs: ListConfig[]
  listContexts: ListContext[]
  isLoading: boolean = true

  constructor(
    readonly cardsService: CardsService,
    readonly cardEvents: CardEvents,
    readonly notifications: NotificationsService,
  ) { 
    this.listConfigs = [
      { id: 'ToDo', title: 'To Do', cards: [], allowAdd: true },
      { id: 'Doing', title: 'Doing', cards: [], },
      { id: 'Done', title: 'Done', cards: [], },
    ]

    this.listContexts = this.listConfigs.map((_, listIndex) => this.getListContext(listIndex))

    this.cardsService.listAll()
      .pipe(
        finalize(() => this.isLoading = false)
      )
      .subscribe(
        result => this.orderCards(result),
        error => {
          this.notifications.show('error', error)
        }
      )
    
    this.cardEvents.cardMoving.pipe(
      untilDestroyed(this)
    ).subscribe(cardMoving => this.moveCard(cardMoving))

    this.cardEvents.cardRemoving.pipe(
      untilDestroyed(this)
    ).subscribe(cardRemoving => this.removeCard(cardRemoving))
    
  }

  ngOnInit(): void {
  }

  orderCards(cardListResult: CardListResult) {

    const groupedCards = groupBy(cardListResult.cards, c => c.listId)
    for (let listConfig of this.listConfigs) {
      listConfig.cards = groupedCards[listConfig.id] || []
      this.sortList(listConfig)
    }
  }

  moveCard(cardMoving: CardMoving) {
    this.removeCard(cardMoving)
    const targetListConfig = this.listConfigs.find(
      listConfig => listConfig.id === cardMoving.targetListId)
    
    if (!targetListConfig) {
      return
    }

    targetListConfig.cards.push(cardMoving.card)
    this.sortList(targetListConfig)
  }

  removeCard(cardRemoving: CardRemoving) {
    const sourceListConfig = this.listConfigs.find(
      listConfig => listConfig.id === cardRemoving.sourceListId)

    if (!sourceListConfig) {
      return
    }

    const cardIndex = sourceListConfig.cards.findIndex(
      card => card.id === cardRemoving.card.id)

    if (cardIndex === -1) {
      return
    }

    sourceListConfig.cards.splice(cardIndex, 1)
  }

  sortList(listConfig: ListConfig) {

    function cardCompare(c1: Card, c2: Card) {
      return c1.id.localeCompare(c2.id)
    }

    listConfig.cards.sort(cardCompare)
  }

  getListContext(listIndex: number): ListContext {
    const lastListIndex = this.listConfigs.length - 1
    const current = this.listConfigs[listIndex]
    const first = this.listConfigs[0]
    const last = this.listConfigs[lastListIndex]
    const next = listIndex === lastListIndex ? null : this.listConfigs[listIndex + 1]
    const previous = listIndex === 0 ? null : this.listConfigs[listIndex - 1]

    return new ListContext(
      current,
      first,
      last,
      next,
      previous
    )
  }

}
