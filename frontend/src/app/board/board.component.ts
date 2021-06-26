import { Component, OnInit } from '@angular/core';
import { Card, CardListResult } from '@/app/card/card';
import { CardsService } from '@/app/card/cards.service';
import { ListConfig } from './list/list-config';
import { groupBy } from '@/helpers/list-helpers';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { finalize, tap } from 'rxjs/operators';

@UntilDestroy()
@Component({
  selector: 'app-board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.scss']
})
export class BoardComponent implements OnInit {

  lists: ListConfig[]
  isLoading: boolean = true

  constructor(
    readonly cardsService: CardsService
  ) { 
    this.lists = [
      { id: 'ToDo', title: 'To Do', cards: [], },
      { id: 'Doing', title: 'Doing', cards: [], },
      { id: 'Done', title: 'Done', cards: [], },
    ]

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

    function cardCompare(c1: Card, c2: Card) {
      return c1.title.localeCompare(c2.title)
    }

    const groupedCards = groupBy(cardListResult.cards, c => c.listId)
    for (let list of this.lists) {
      list.cards = groupedCards[list.id].sort(cardCompare)
    }
  }

}
