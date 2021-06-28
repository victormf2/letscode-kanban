import { ListContext } from '@/app/board/list/list-context';
import { NotificationsService } from '@/app/notifications/notifications.service';
import { AfterViewInit, Component, ElementRef, ViewChild } from '@angular/core';
import { Card } from '../card';
import { CardEvents } from '../card-events';
import { CardManager } from '../card-manager';
import { CardsService } from '../cards.service';

@Component({
  selector: 'app-card-view',
  templateUrl: './card-view.component.html',
  styleUrls: ['./card-view.component.scss']
})
export class CardViewComponent implements AfterViewInit {

  @ViewChild('content') contentEl!: ElementRef<HTMLDivElement>

  isUpdating: boolean = false
  showContextMenu: boolean = false

  constructor(
    readonly manager: CardManager,
    readonly listContext: ListContext,
    readonly cardsService: CardsService,
    readonly cardEvents: CardEvents,
    readonly notifications: NotificationsService
  ) { 
  }

  ngAfterViewInit(): void {
    const contentWrapper = this.contentEl.nativeElement;
    const contentElement = contentWrapper.firstChild as HTMLParagraphElement;
    const contentIsTooBig = contentWrapper.clientHeight < contentElement.clientHeight

    if (contentIsTooBig) {
      contentWrapper.parentElement!.classList.add('content-too-big')
    }
  }

  toggleContextMenu() {
    // this is required because of clickOutside behavior
    // angular instantiates context menu during event bubbling and not after
    setTimeout(() => {
      this.showContextMenu = !this.showContextMenu
    }, 0)
  }

  previousStep(): void {
    const previousListId = this.listContext.previous?.id
    if (!previousListId) {
      return
    }
    this.requestCardMove(previousListId)
  }

  nextStep(): void {
    const nextListId = this.listContext.next?.id
    if (!nextListId) {
      return
    }
    this.requestCardMove(nextListId)
  }

  private requestCardMove(listId: string) {
    this.isUpdating = true
    const cardToMove: Card = {
      ...this.manager.card,
      listId
    }
    this.cardsService.update(cardToMove).subscribe(
      result => {
        this.manager.card = result
        this.cardEvents.cardMoving.next({ 
          card: this.manager.card,
          sourceListId: this.listContext.current.id,
          targetListId: this.manager.card.listId 
        })
      },
      error => {
        this.notifications.show('error', error)
        this.isUpdating = false
      }
    )
  }

}
