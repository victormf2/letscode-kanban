import { Card } from '@/app/card/card';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CardMoving } from '../../card/card-moving';
import { ListConfig } from './list-config';
import { ListContext } from './list-context';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss'],
})
export class ListComponent implements OnInit {
  
  @Input() config: ListConfig
  @Input() context!: ListContext
  @Output() cardMoving = new EventEmitter<CardMoving>()

  constructor() { 
    this.config = {
      id: '',
      title: '',
      cards: [],
    }
  }


  ngOnInit(): void {
  }

  removeCard(card: Card) {
    const cardIndex = this.config.cards.findIndex(c => c.id === card.id)
    this.config.cards.splice(cardIndex, 1)
  }

  notifyCardMoving(cardMoving: CardMoving) {
    this.cardMoving.emit(cardMoving)
  }

}
