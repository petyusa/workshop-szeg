import { Routes } from '@angular/router';
import { authGuard } from './guards/auth.guard';
import { LoginComponent } from './components/login/login';

export const routes: Routes = [
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: '',
    canActivate: [authGuard],
    children: [
      {
        path: '',
        loadComponent: () => import('./components/home/home').then(m => m.HomeComponent)
      },
      {
        path: 'my-reservations',
        loadComponent: () => import('./components/my-reservations/my-reservations').then(m => m.MyReservationsComponent)
      }
    ]
  },
  {
    path: '**',
    redirectTo: ''
  }
];
