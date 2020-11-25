import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './_guards/auth.guard';
import { TicketsComponent } from './tickets/tickets.component';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent},
    {
        path: '',
        runGuardsAndResolvers: 'always',
         canActivate: [AuthGuard],
        children: [
            { path: 'tickets', component: TicketsComponent},
            { path: 'admin', component: AdminPanelComponent, data: {roles: ['Admin', 'Moderator']}},
        ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full'}
];
