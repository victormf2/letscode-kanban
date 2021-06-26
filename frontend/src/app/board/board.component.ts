import { Card, CardListResult } from '@/app/card/card';
import { CardsService } from '@/app/card/cards.service';
import { groupBy } from '@/helpers/list-helpers';
import { Component, OnInit } from '@angular/core';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { finalize } from 'rxjs/operators';
import { CardMoving } from '../card/card-moving';
import { ListConfig } from './list/list-config';
import { ListContext } from './list/list-context';

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
    readonly cardsService: CardsService
  ) { 
    this.listConfigs = [
      { id: 'ToDo', title: 'To Do', cards: [], allowAdd: true },
      { id: 'Doing', title: 'Doing', cards: [], },
      { id: 'Done', title: 'Done', cards: [], },
    ]

    this.listContexts = this.listConfigs.map((_, listIndex) => this.getListContext(listIndex))

    this.cardsService.listAll()
      .pipe(
        untilDestroyed(this),
        finalize(() => this.isLoading = false)
      )
      .subscribe(
        result => this.orderCards(result),
        error => {/** TODO toast */}
      )
  }

  ngOnInit(): void {
  }

  orderCards(cardListResult: CardListResult) {

    const groupedCards = groupBy(cardListResult.cards, c => c.listId)
    for (let listConfig of this.listConfigs) {
      listConfig.cards = groupedCards[listConfig.id]
      this.sortList(listConfig)
    }
  }

  moveCard(cardMoving: CardMoving) {
    const targetListConfig = this.listConfigs.find(
      listConfig => listConfig.id === cardMoving.targetListId)
    
    if (!targetListConfig) {
      return
    }

    targetListConfig.cards.push(cardMoving.card)
    this.sortList(targetListConfig)
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
