import { NotificationsService } from '@/app/notifications/notifications.service';
import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Card } from '../card';
import { CardValue } from '../card-editor-base/card-value';
import { CardManager } from '../card-manager';
import { CardsService } from '../cards.service';

@Component({
  selector: 'app-card-editor',
  templateUrl: './card-editor.component.html',
  styleUrls: ['./card-editor.component.scss']
})
export class CardEditorComponent implements OnInit {

  cardForm: FormControl

  constructor(
    readonly manager: CardManager,
    readonly cardsService: CardsService,
    readonly notifications: NotificationsService
  ) {
    const formValue: CardValue = {
      title: manager.card.title,
      content: manager.card.content
    }
    this.cardForm = new FormControl(formValue)
  }

  ngOnInit(): void {
  }

  stopEditing() {
    this.manager.mode = 'view'
  }

  save(cardValue: CardValue) {
    this.cardForm.disable()

    const cardToSave: Card = { 
      ...this.manager.card,
      title: cardValue.title,
      content: cardValue.content
    }
    this.cardsService.update(cardToSave).subscribe(
      result => {
        this.manager.card = result
        this.stopEditing()
      },
      error => {
        this.notifications.show('error', error)
        this.cardForm.enable()
      }
    )
  }

}
