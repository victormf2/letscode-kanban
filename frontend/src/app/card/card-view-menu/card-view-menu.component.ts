import { NotificationsService } from '@/app/notifications/notifications.service';
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

  isShowingRemoveModal: boolean
  isRemoving: boolean

  constructor(
    readonly manager: CardManager,
    readonly cardsService: CardsService,
    readonly cardEvents: CardEvents,
    readonly notifications: NotificationsService,
  ) { 
    this.isShowingRemoveModal = false
    this.isRemoving = false
  }

  ngOnInit(): void {
  }

  startEditing(): void {
    this.manager.mode = 'edit'
  }

  toggleRemoveModal() {
    this.isShowingRemoveModal = !this.isShowingRemoveModal
  }

  removeCard() {
    this.isRemoving = true
    this.cardsService.remove(this.manager.card).subscribe(
      _ => {
        this.cardEvents.cardRemoving.next({
          sourceListId: this.manager.card.listId,
          card: this.manager.card
        })
      },
      error => { 
        this.notifications.show('error', error)
        this.isRemoving = false
        this.isShowingRemoveModal = false
      }
    )
  }

}
