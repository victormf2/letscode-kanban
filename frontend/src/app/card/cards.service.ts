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
      { id: '2', title: 'Card2', content: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sagittis faucibus ipsum at aliquet. Vestibulum id enim nunc. Donec sed tincidunt magna.', listId: 'ToDo' },

      { id: '3', title: 'Card3', content: 'Card3 content', listId: 'Done' },
      { id: '4', title: 'Card4', content: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sagittis faucibus ipsum at aliquet. Vestibulum id enim nunc. Donec sed tincidunt magna. Aliquam ac tortor ac lectus dictum tristique eget nec nisl. Mauris placerat tellus id posuere malesuada. Proin eros arcu, condimentum et tortor sed, sollicitudin sodales magna. Pellentesque in ante ut ipsum blandit ornare eu ut mi. Ut neque orci, aliquam eget rhoncus eget, interdum ut ex. Donec nec odio efficitur, molestie elit ac, commodo nisl. Curabitur felis elit, consequat quis elit sit amet, pulvinar varius neque. Donec sed turpis nulla. Curabitur ac congue elit, eu imperdiet diam. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.', listId: 'Doing' },

      { id: '5', title: 'Card5', content: 'Card5 content', listId: 'Done' },
      { id: '6', title: 'Card6', content: 'Card6 content', listId: 'Done' },

    ]
  }

  private mockError() {
    return timer(2000).pipe(
      switchMap(() => throwError(new Error()))
    )
  }

  add(card: NewCard): Observable<Card> {
    return of(card).pipe(
      delay(200),
      map(c => ({ id: 'x', ...c }))
    )
  }

  update(card: Card): Observable<Card> {
    return of(card).pipe(
      delay(3000),
    )
  }

  remove(card: Card): Observable<CardListResult> {
    return of(this.mockCards).pipe(
      delay(200),
    )
  }

  listAll(): Observable<CardListResult> {
    return of(this.mockCards).pipe(
      delay(0),
    )
  }
}
