import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ListContext } from '@/app/board/list/list-context';
import { ListComponent } from '@/app/board/list/list.component';
import { Card } from './card';
import { CardManager } from './card-manager';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.scss'],
  host: {
    class: 'card'
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

  @Input() 
  set card(value: Card) {
    this.manager.card = value
  }

  constructor(
    readonly manager: CardManager
  ) {
  }

}
