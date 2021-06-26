import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardComponent } from './card.component';
import { CardViewComponent } from './card-view/card-view.component';
import { CardEditorComponent } from './card-editor/card-editor.component';
import { ReactiveFormsModule } from '@angular/forms';
import { LoadingFooterComponent } from './loading-footer/loading-footer.component';
import { CardViewMenuComponent } from './card-view-menu/card-view-menu.component';
import { HelpersModule } from '@/app/helpers/helpers.module';
import { NewCardComponent } from './new-card/new-card.component';
import { CardEditorBaseComponent } from './card-editor-base/card-editor-base.component';



@NgModule({
  declarations: [
    CardComponent,
    CardViewComponent,
    CardEditorComponent,
    LoadingFooterComponent,
    CardViewMenuComponent,
    NewCardComponent,
    CardEditorBaseComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    HelpersModule,
  ],
  exports: [
    CardComponent,
    NewCardComponent,
  ]
})
export class CardModule { }
