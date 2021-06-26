import { Component, Input, OnInit } from '@angular/core';
import { Card } from './card';
import { CardManager } from './card-manager';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.scss'],
  host: {
    'class': 'card'
  },
  providers: [CardManager]
})
export class CardComponent {

  @Input() 
  set card(value: Card) {
    this.manager.card = value
  }

  constructor(
    readonly manager: CardManager
  ) {
  }

}
