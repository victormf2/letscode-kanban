import { ListContext } from '@/app/board/list/list-context';
import { Component, Input, ViewChild, ElementRef, AfterViewInit, Output, EventEmitter } from '@angular/core';
import { Card } from '../card';
import { CardManager } from '../card-manager';
import { CardMoving } from '../card-moving';
import { CardsService } from '../cards.service';

@Component({
  selector: 'app-card-view',
  templateUrl: './card-view.component.html',
  styleUrls: ['./card-view.component.scss']
})
export class CardViewComponent implements AfterViewInit {

  @ViewChild('content') contentEl!: ElementRef<HTMLDivElement>
  @Output() cardMoving = new EventEmitter<CardMoving>()

  isUpdating: boolean = false
  showContextMenu: boolean = false

  constructor(
    readonly manager: CardManager,
    readonly listContext: ListContext,
    readonly cardsService: CardsService,
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
    this.showContextMenu = !this.showContextMenu
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
        this.cardMoving.emit({ 
          card: this.manager.card, 
          targetListId: this.manager.card.listId 
        })
      },
      error => {
        /** TODO toast */
        this.isUpdating = false
      }
    )
  }

}
