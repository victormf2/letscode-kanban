import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
import { NewCard } from '../card';
import { CardValue } from '../card-editor-base/card-value';
import { CardMoving } from '../card-moving';
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
  @Output() cardMoving = new EventEmitter<CardMoving>()
  @Output() cancel = new EventEmitter()

  cardForm: FormControl

  constructor(
    readonly cardsService: CardsService
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
        this.cardMoving.emit({
          targetListId: this.listId,
          card: result
        })
      }
    )
  }

  stopEditing() {
    this.cancel.emit()
  }

}
