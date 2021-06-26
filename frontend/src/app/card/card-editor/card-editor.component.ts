import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Card } from '../card';
import { CardManager } from '../card-manager';
import { CardsService } from '../cards.service';

@Component({
  selector: 'app-card-editor',
  templateUrl: './card-editor.component.html',
  styleUrls: ['./card-editor.component.scss']
})
export class CardEditorComponent implements OnInit {

  cardForm!: FormGroup
  
  constructor(
    readonly manager: CardManager,
    readonly cardsService: CardsService
  ) {
  }

  ngOnInit(): void {
    this.cardForm = new FormGroup({
      title: new FormControl(this.manager.card.title),
      content: new FormControl(this.manager.card.content),
    })
  }

  stopEditing() {
    this.manager.mode = 'view'
  }

  save() {
    this.cardForm.disable()

    const cardValues = this.cardForm.value
    const cardToSave: Card = { 
      ...this.manager.card,
      title: cardValues.title,
      content: cardValues.content
    }
    this.cardsService.update(cardToSave).subscribe(
      result => {
        this.manager.card = result
        this.stopEditing()
      },
      error => {
        /** TODO toast */
        this.cardForm.enable()
      }
    )
  }

}
