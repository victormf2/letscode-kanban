import { Component, Input, OnInit } from '@angular/core';
import { ListConfig } from './list-config';
import { ListContext } from './list-context';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss'],
})
export class ListComponent implements OnInit {
  
  @Input() config: ListConfig
  @Input() context!: ListContext

  isShowingNewCard: boolean

  constructor() { 
    this.isShowingNewCard = false
    this.config = {
      id: '',
      title: '',
      cards: [],
    }
  }


  ngOnInit(): void {
  }

  showNewCard() {
    this.isShowingNewCard = true
  }

  hideNewCard() {
     this.isShowingNewCard = false
  }

}
