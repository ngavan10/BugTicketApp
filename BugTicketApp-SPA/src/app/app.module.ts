import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { appRoutes } from './routes';
import { AuthServiceService } from './services/auth-service.service';
import { AlertifyService } from './services/alertify.service';
import { JwtModule } from '@auth0/angular-jwt';
import { TicketsComponent } from './tickets/tickets.component';
import { AuthGuard } from './_guards/auth.guard';
import { ErrorInterceptorProvider } from './services/error.interceptor';
import { ModalModule } from 'ngx-bootstrap/modal';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { HasRoleDirective } from '../app/_directives/hasRole.directive';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { UserManagementComponent } from './admin/user-management/user-management.component';
import { RolesModalComponent } from './admin/roles-modal/roles-modal.component';
import { RegisterComponent } from './admin/register/register.component';

export function tokenGetter() {
  return localStorage.getItem('token');
}

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    TicketsComponent,
    HasRoleDirective,
    AdminPanelComponent,
    UserManagementComponent,
    RolesModalComponent,
    RegisterComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    TabsModule.forRoot(),
    ModalModule.forRoot(),
    RouterModule.forRoot(appRoutes),
    JwtModule.forRoot({
      config: {
         tokenGetter,
         allowedDomains: ['localhost:5000'],
         disallowedRoutes: ['localhost:5000/api/auth']
      }
   })
  ],
  providers: [
    AuthServiceService,
    AlertifyService,
    AuthGuard,
    ErrorInterceptorProvider
  ],
  entryComponents: [
    RolesModalComponent
 ],
  bootstrap: [AppComponent]
})
export class AppModule { }
