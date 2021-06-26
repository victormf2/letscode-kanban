import { Component, Input, OnInit } from '@angular/core';
import { ListConfig } from './list-config';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements OnInit {
  
  @Input() config: ListConfig

  constructor() { 
    this.config = {
      id: '',
      title: '',
      cards: [],
    }
  }


  ngOnInit(): void {
  }

}
