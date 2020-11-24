import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './_guards/auth.guard';
import { TicketsComponent } from './tickets/tickets.component';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent},
    {
        path: '',
        runGuardsAndResolvers: 'always',
         canActivate: [AuthGuard],
        children: [
            { path: 'tickets', component: TicketsComponent}
        ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full'}
];
