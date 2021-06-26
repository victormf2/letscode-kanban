import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BoardRoutingModule } from './board-routing.module';
import { BoardComponent } from './board.component';
import { ListComponent } from './list/list.component';
import { CardModule } from '@/app/card/card.module';


@NgModule({
  declarations: [
    BoardComponent,
    ListComponent
  ],
  imports: [
    CommonModule,
    BoardRoutingModule,
    CardModule
  ]
})
export class BoardModule { }
