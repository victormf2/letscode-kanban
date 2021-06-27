import { Component, OnInit } from '@angular/core';
import { CardEvents } from '../card-events';
import { CardManager } from '../card-manager';
import { CardsService } from '../cards.service';

@Component({
  selector: 'app-card-view-menu',
  templateUrl: './card-view-menu.component.html',
  styleUrls: ['./card-view-menu.component.scss'],
  host: {
    class: 'box'
  }
})
export class CardViewMenuComponent implements OnInit {

  constructor(
    readonly manager: CardManager,
    readonly cardsService: CardsService,
    readonly cardEvents: CardEvents
  ) { }

  ngOnInit(): void {
  }

  startEditing(): void {
    this.manager.mode = 'edit'
  }

  removeCard() {
    this.cardsService.remove(this.manager.card).subscribe(
      _ => {
        this.cardEvents.cardRemoving.next({
          sourceListId: this.manager.card.listId,
          card: this.manager.card
        })
      },
      error => { /** TODO toast */}
    )
  }

}
