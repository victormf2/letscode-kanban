import { Component, Input } from '@angular/core';
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

  @Input() card!: Card

  constructor(
    readonly manager: CardManager
  ) {
    this.card = {
      id: '',
      title: '',
      content: '',
      listId: ''
    }
  }

}
