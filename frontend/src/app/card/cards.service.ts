import { Injectable } from '@angular/core';
import { Observable, of, throwError, timer } from 'rxjs';
import { Card, CardListResult, NewCard } from './card';
import { delay, map, switchMap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CardsService {

  constructor() { }

  private mockCards: CardListResult = {
    cards: [
      { id: '1', title: 'Dard1', content: 'Card1 content', listId: 'ToDo' },
      { id: '2', title: 'Card2', content: 'Card2 content', listId: 'ToDo' },

      { id: '3', title: 'Card3', content: 'Card3 content', listId: 'Done' },
      { id: '4', title: 'Card4', content: 'Card4 content', listId: 'Doing' },

      { id: '5', title: 'Card5', content: 'Card5 content', listId: 'Done' },
      { id: '6', title: 'Card6', content: 'Card6 content', listId: 'Done' },

    ]
  }

  add(card: NewCard): Observable<Card> {
    return of(card).pipe(
      delay(200),
      map(c => ({ id: 'x', ...c }))
    )
  }

  update(card: Card): Observable<Card> {
    return of(card).pipe(
      delay(200),
    )
  }

  remove(card: Card): Observable<CardListResult> {
    return of(this.mockCards).pipe(
      delay(200),
    )
  }

  listAll(): Observable<CardListResult> {
    // return timer(2000).pipe(
    //   switchMap(() => throwError(new Error()))
    // )

    return of(this.mockCards).pipe(
      delay(200),
    )
  }
}
