import { Injectable } from "@angular/core";

type CardMode = 'view' | 'edit'

@Injectable()
export class CardManager {
  mode: CardMode

  constructor() {
    this.mode = 'view'
  }
}