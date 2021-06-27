import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
import { NewCard } from '../card';
import { CardValue } from '../card-editor-base/card-value';
import { CardEvents } from '../card-events';
import { CardsService } from '../cards.service';

@Component({
  selector: 'app-new-card',
  templateUrl: './new-card.component.html',
  styleUrls: ['./new-card.component.scss'],
  host: {
    class: 'card'
  }
})
export class NewCardComponent implements OnInit {
  
  @Input() listId!: string
  @Output() hide = new EventEmitter()

  cardForm: FormControl

  constructor(
    readonly cardsService: CardsService,
    readonly cardEvents: CardEvents
  ) { 
    const formValue: CardValue = {
      title: '',
      content: ''
    }
    this.cardForm = new FormControl(formValue)
  }

  ngOnInit(): void {
  }

  save(cardValue: CardValue) {
    this.cardForm.disable()

    const newCard: NewCard = {
      ...cardValue,
      listId: this.listId
    }

    this.cardsService.add(newCard).subscribe(
      result => {
        this.cardEvents.cardMoving.next({
          card: result,
          targetListId: this.listId,
          sourceListId: ''
        })
        this.hide.emit()
      },
      error => { /** TODO toast */}
    )
  }

  stopEditing() {
    this.hide.emit()
  }

}
