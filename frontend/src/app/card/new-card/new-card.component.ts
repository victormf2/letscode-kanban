import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-new-card',
  templateUrl: './new-card.component.html',
  styleUrls: ['./new-card.component.scss'],
  host: {
    class: 'card'
  }
})
export class NewCardComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
