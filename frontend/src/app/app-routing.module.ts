import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthenticationGuard } from './authentication.guard';

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'board' },
  { 
    path: 'board', 
    loadChildren: () => import('./board/board.module').then(m => m.BoardModule),
    canActivate: [AuthenticationGuard]
  },
  { 
    path: 'login', 
    loadChildren: () => import('./login/login.module').then(m => m.LoginModule) 
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
