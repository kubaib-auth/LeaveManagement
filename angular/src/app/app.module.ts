import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientJsonpModule } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { ModalModule } from 'ngx-bootstrap/modal';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { NgxPaginationModule } from 'ngx-pagination';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ServiceProxyModule } from '@shared/service-proxies/service-proxy.module';
import { SharedModule } from '@shared/shared.module';
import { HomeComponent } from '@app/home/home.component';
import { AboutComponent } from '@app/about/about.component';
// tenants
import { TenantsComponent } from '@app/tenants/tenants.component';
import { CreateTenantDialogComponent } from './tenants/create-tenant/create-tenant-dialog.component';
import { EditTenantDialogComponent } from './tenants/edit-tenant/edit-tenant-dialog.component';
// roles
import { RolesComponent } from '@app/roles/roles.component';
import { CreateRoleDialogComponent } from './roles/create-role/create-role-dialog.component';
import { EditRoleDialogComponent } from './roles/edit-role/edit-role-dialog.component';
// users
import { UsersComponent } from '@app/users/users.component';
import { CreateUserDialogComponent } from '@app/users/create-user/create-user-dialog.component';
import { EditUserDialogComponent } from '@app/users/edit-user/edit-user-dialog.component';
import { ChangePasswordComponent } from './users/change-password/change-password.component';
import { ResetPasswordDialogComponent } from './users/reset-password/reset-password.component';
// layout
import { HeaderComponent } from './layout/header.component';
import { HeaderLeftNavbarComponent } from './layout/header-left-navbar.component';
import { HeaderLanguageMenuComponent } from './layout/header-language-menu.component';
import { HeaderUserMenuComponent } from './layout/header-user-menu.component';
import { FooterComponent } from './layout/footer.component';
import { SidebarComponent } from './layout/sidebar.component';
import { SidebarLogoComponent } from './layout/sidebar-logo.component';
import { SidebarUserPanelComponent } from './layout/sidebar-user-panel.component';
import { SidebarMenuComponent } from './layout/sidebar-menu.component';
//import { LeaveModule } from 'leave/leave.module';
import { LeaveComponent } from './leave/leave.component';
// import { LeaveComponent } from 'leave/leave.component';
import { TreeTableModule } from 'primeng/treetable'; 
import { ButtonModule } from 'primeng/button'; 
import { DialogModule } from 'primeng/dialog'; 
import { MultiSelectModule } from 'primeng/multiselect'; 
import { InputTextModule } from 'primeng/inputtext'; 
import { ToastModule } from 'primeng/toast'; 
import { ContextMenuModule } from 'primeng/contextmenu'; 
import { TableModule } from 'primeng/table';
import { CreateOrEditLeaveComponent } from './leave/create-or-edit-leave.component';
import { DropdownModule } from 'primeng/dropdown';
import { CalendarModule } from 'primeng/calendar';
import { RadioButtonModule } from 'primeng/radiobutton'; 
import { BrowserAnimationsModule, NoopAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
//import { BrowserAnimationsModule } from '@angular/platform-browser/animations'; 
import { InputTextareaModule } from 'primeng/inputtextarea';
import {InputSwitchModule} from 'primeng/inputswitch';
import {DatePipe} from '@angular/common';
import { ApproveOrRejectedleavComponent } from './leave/approve-or-rejectedleav.component';
import { PermissionModelComponent } from './users/permission-model.component';
import { NotificationComponent } from './layout/notification.component';
import { UserDashboardComponent } from './leave/user-dashboard.component';
import { ChartModule } from 'primeng/chart';
import { ViewNotificationComponent } from './layout/view-notification.component';
import { LeavecateoryComponent } from './leave/leavecateory.component';
import { CreateOrEditleaveCateoryComponent } from './leave/create-or-editleave-cateory.component';


@NgModule({
    declarations: [
        AppComponent,
        HomeComponent,
        AboutComponent,
        // tenants
        TenantsComponent,
        CreateTenantDialogComponent,
        EditTenantDialogComponent,
        // roles
        RolesComponent,
        CreateRoleDialogComponent,
        EditRoleDialogComponent,
        // users
        UsersComponent,
        CreateUserDialogComponent,
        EditUserDialogComponent,
        ChangePasswordComponent,
        ResetPasswordDialogComponent,
        // layout
        HeaderComponent,
        HeaderLeftNavbarComponent,
        HeaderLanguageMenuComponent,
        HeaderUserMenuComponent,
        FooterComponent,
        SidebarComponent,
        SidebarLogoComponent,
        SidebarUserPanelComponent,
        SidebarMenuComponent,
        LeaveComponent,
        CreateOrEditLeaveComponent,
        ApproveOrRejectedleavComponent,
        PermissionModelComponent,
        NotificationComponent,
        UserDashboardComponent,
        ViewNotificationComponent,
        LeavecateoryComponent,
        CreateOrEditleaveCateoryComponent
    ],
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        HttpClientJsonpModule,
        ModalModule.forChild(),
        BsDropdownModule,
        DropdownModule,
        CollapseModule,
        TabsModule,
        AppRoutingModule,
        ServiceProxyModule,
        SharedModule,
        NgxPaginationModule,
        TreeTableModule, 
        ToastModule, 
        TableModule, 
        DialogModule, 
        ButtonModule, 
        MultiSelectModule, 
        InputTextModule, 
        ContextMenuModule, 
        CalendarModule,
        RadioButtonModule,
        InputTextareaModule,
        InputSwitchModule,
       // ChartModule,
       // LeaveModule
        
    ],
    exports: [
        // LeaveComponent 
       // ChartModule
      ],
    providers: [DatePipe]
})
export class AppModule {}
