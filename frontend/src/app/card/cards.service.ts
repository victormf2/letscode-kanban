import { api } from '@/helpers/api-helpers';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Card, CardListResult, NewCard } from './card';

@Injectable({
  providedIn: 'root'
})
export class CardsService {

  constructor(
    private readonly httpClient: HttpClient
  ) { }

  add(card: NewCard): Observable<Card> {
    return this.httpClient.post<Card>(api('cards'), card)
  }

  update(card: Card): Observable<Card> {
    return this.httpClient.put<Card>(api(`cards/${card.id}`), card)
  }

  remove(card: Card): Observable<CardListResult> {
    return this.httpClient.delete<CardListResult>(api(`cards/${card.id}`))
  }

  listAll(): Observable<CardListResult> {
    return this.httpClient.get<CardListResult>(api('cards'))
  }
}
