import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ListContext } from '@/app/board/list/list-context';
import { ListComponent } from '@/app/board/list/list.component';
import { Card } from './card';
import { CardManager } from './card-manager';
import { CardMoving } from './card-moving';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.scss'],
  host: {
    'class': 'card'
  },
  providers: [
    CardManager,
    { 
      provide: ListContext,
      deps: [ListComponent],
      useFactory: function(listComponent: ListComponent) {
        return listComponent.context
      }
    }
  ]
})
export class CardComponent {

  @Output() cardMoving = new EventEmitter<CardMoving>()

  @Input() 
  set card(value: Card) {
    this.manager.card = value
  }

  constructor(
    readonly manager: CardManager
  ) {
  }

  notifyCardMoving(cardMoving: CardMoving) {
    this.cardMoving.emit(cardMoving)
  }

}
