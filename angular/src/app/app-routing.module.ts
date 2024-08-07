import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { HomeComponent } from './home/home.component';
import { AboutComponent } from './about/about.component';
import { UsersComponent } from './users/users.component';
import { TenantsComponent } from './tenants/tenants.component';
import { RolesComponent } from 'app/roles/roles.component';
import { ChangePasswordComponent } from './users/change-password/change-password.component';
import { LeaveComponent } from './leave/leave.component';
import { CreateOrEditLeaveComponent } from './leave/create-or-edit-leave.component';
import { UserDashboardComponent } from './leave/user-dashboard.component';
import { ViewNotificationComponent } from './layout/view-notification.component';
import { LeavecateoryComponent } from './leave/leavecateory.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                component: AppComponent,
                children: [
                    { path: 'user-dashboard', component: UserDashboardComponent, canActivate: [AppRouteGuard] },

                    { path: 'home', component: HomeComponent,  canActivate: [AppRouteGuard] },
                    { path: 'users', component: UsersComponent, data: { permission: 'Pages.Users' }, canActivate: [AppRouteGuard] },
                    { path: 'roles', component: RolesComponent, data: { permission: 'Pages.Roles' }, canActivate: [AppRouteGuard] },
                    { path: 'tenants', component: TenantsComponent, data: { permission: 'Pages.Tenants' }, canActivate: [AppRouteGuard] },
                    { path: 'about', component: AboutComponent, canActivate: [AppRouteGuard] },
                    { path: 'update-password', component: ChangePasswordComponent, canActivate: [AppRouteGuard] },
                    { path: 'leave', component: LeaveComponent, canActivate: [AppRouteGuard] },
                    { path: 'createOrEditleave', component: CreateOrEditLeaveComponent, canActivate: [AppRouteGuard] },
                    { path: 'view-notification', component: ViewNotificationComponent, canActivate: [AppRouteGuard] },
                    { path: 'leavecateory', component: LeavecateoryComponent, canActivate: [AppRouteGuard] }

                ]
            }
        ])
    ],
    exports: [RouterModule]
})
export class AppRoutingModule { }
