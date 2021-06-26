import { Component, OnInit } from '@angular/core';
import { CardManager } from '../card-manager';

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
    readonly manager: CardManager
  ) { }

  ngOnInit(): void {
  }

  startEditing(): void {
    this.manager.mode = 'edit'
  }

  removeCard() {
    
  }

}
