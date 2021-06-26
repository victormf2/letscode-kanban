import { Component, Input, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { Card } from '../card';
import { CardManager } from '../card-manager';

@Component({
  selector: 'app-card-view',
  templateUrl: './card-view.component.html',
  styleUrls: ['./card-view.component.scss']
})
export class CardViewComponent implements AfterViewInit {

  @ViewChild('content') contentEl!: ElementRef<HTMLDivElement>

  card: Card

  constructor(
    readonly manager: CardManager
  ) { 
    this.card = this.manager.card
  }

  ngAfterViewInit(): void {
    const contentWrapper = this.contentEl.nativeElement;
    const contentElement = contentWrapper.firstChild as HTMLParagraphElement;
    const contentIsTooBig = contentWrapper.clientHeight < contentElement.clientHeight

    if (contentIsTooBig) {
      contentWrapper.parentElement!.classList.add('content-too-big')
    }
  }

  startEditing(): void {
    this.manager.mode = 'edit'
  }

}
