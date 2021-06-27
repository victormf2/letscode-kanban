import { Injectable } from "@angular/core";
import { Subject } from "rxjs";
import { Card } from "./card";

export interface CardMoving {
  card: Card
  sourceListId: string
  targetListId: string
}

export interface CardRemoving {
  card: Card
  sourceListId: string
}

@Injectable({ 
  providedIn: 'root'
})
export class CardEvents {
  cardMoving: Subject<CardMoving>
  cardRemoving: Subject<CardRemoving>

  constructor() {
    this.cardMoving = new Subject
    this.cardRemoving = new Subject
  }
}