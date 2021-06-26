import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardComponent } from './card.component';
import { CardViewComponent } from './card-view/card-view.component';
import { CardEditorComponent } from './card-editor/card-editor.component';



@NgModule({
  declarations: [
    CardComponent,
    CardViewComponent,
    CardEditorComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    CardComponent
  ]
})
export class CardModule { }
