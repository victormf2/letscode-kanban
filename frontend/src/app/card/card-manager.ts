import { Injectable } from "@angular/core";
import { Card } from "./card";

type CardMode = 'view' | 'edit'

@Injectable()
export class CardManager {
  
  mode: CardMode
  card: Card

  constructor() {
    this.mode = 'view'
    this.card = {
      id: '',
      title: '',
      content: '',
      listId: ''
    }
  }
}