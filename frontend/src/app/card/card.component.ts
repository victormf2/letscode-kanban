import { Component, Input, OnInit } from '@angular/core';
import { Card } from './card';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.scss'],
  host: {
    'class': 'card'
  }
})
export class CardComponent implements OnInit {

  @Input() card: Card

  constructor() { 
    this.card = {
      id: '',
      title: '',
      content: '',
      listId: ''
    }
  }

  ngOnInit(): void {
  }

}
