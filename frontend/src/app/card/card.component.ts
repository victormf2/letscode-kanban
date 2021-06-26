import { AfterViewInit, Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { Card } from './card';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.scss'],
  host: {
    'class': 'card'
  }
})
export class CardComponent implements OnInit, AfterViewInit {

  @Input() card: Card
  @ViewChild('content') contentEl!: ElementRef<HTMLDivElement>

  constructor() { 
    this.card = {
      id: '',
      title: '',
      content: '',
      listId: ''
    }
  }

  ngOnInit(): void {
    //[class.content-too-big]="contentWrapper.clientHeight < content.clientHeight"
  }
  ngAfterViewInit(): void {

    debugger
    const contentWrapper = this.contentEl.nativeElement;
    const contentElement = contentWrapper.firstChild as HTMLParagraphElement;
    const contentIsTooBig = contentWrapper.clientHeight < contentElement.clientHeight

    if (contentIsTooBig) {
      contentWrapper.parentElement!.classList.add('content-too-big')
    }
    //throw new Error('Method not implemented.');
  }

}
