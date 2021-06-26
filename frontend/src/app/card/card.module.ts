import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardComponent } from './card.component';
import { CardViewComponent } from './card-view/card-view.component';
import { CardEditorComponent } from './card-editor/card-editor.component';
import { ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    CardComponent,
    CardViewComponent,
    CardEditorComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
  ],
  exports: [
    CardComponent
  ]
})
export class CardModule { }
