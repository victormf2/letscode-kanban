import { Component, Input, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { Card } from '../card';
import { CardManager } from '../card-manager';

@Component({
  selector: 'app-card-view',
  templateUrl: './card-view.component.html',
  styleUrls: ['./card-view.component.scss']
})
export class CardViewComponent implements AfterViewInit {

  @Input() card!: Card
  @ViewChild('content') contentEl!: ElementRef<HTMLDivElement>

  constructor(
    readonly manager: CardManager
  ) { }

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
