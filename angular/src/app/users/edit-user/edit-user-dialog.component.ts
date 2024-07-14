import {
  Component,
  Injector,
  OnInit,
  EventEmitter,
  Output
} from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { forEach as _forEach, includes as _includes, map as _map } from 'lodash-es';
import { AppComponentBase } from '@shared/app-component-base';
import {
  UserServiceProxy,
  UserDto,
  RoleDto,
  PermissionDto,
  RoleEditDto,
  GetRoleForEditOutput,
  FlatPermissionDto,
  RoleServiceProxy,
  CreateUserDto
} from '@shared/service-proxies/service-proxies';
import { NgForm } from '@angular/forms';

@Component({
  templateUrl: './edit-user-dialog.component.html'
})
export class EditUserDialogComponent extends AppComponentBase
  implements OnInit {
  saving = false;
  user = new UserDto();
  roles: RoleDto[] = [];
  checkedRolesMap: { [key: string]: boolean } = {};
  grantedPermissionNames: string[];
  checkedPermissionsMap: { [key: string]: boolean } = {};
  role = new RoleEditDto();
  permissions: FlatPermissionDto[];

  id: number;
  leaveStatus=[];
  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
    public _userService: UserServiceProxy,
    private _roleService: RoleServiceProxy,

    public bsModalRef: BsModalRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this._userService.get(this.id).subscribe((result) => {
      this.user = result;
      const joiningDateString = this.user.joiningDate.toISOString();
      this.user.joiningDate = this.converToDateFormate(joiningDateString);
      this._userService.getRoles().subscribe((result2) => {
        this.roles = result2.items;
        this.setInitialRolesStatus();
      });
    });
    this.getUserLeaveStatus();
    // this._roleService
    //   .getRoleForEdit(this.id)
    //   .subscribe((result: GetRoleForEditOutput) => {
    //     this.role = result.role;
    //     this.permissions = result.permissions;
    //     this.grantedPermissionNames = result.grantedPermissionNames;
    //     this.setInitialPermissionsStatus();
    //   });
  }
  converToDateFormate(date: string): any{
    return new Date(date);
   }
  setInitialRolesStatus(): void {
    _map(this.roles, (item) => {
      this.checkedRolesMap[item.normalizedName] = this.isRoleChecked(
        item.normalizedName
      );
    });
  }

  isRoleChecked(normalizedName: string): boolean {
    return _includes(this.user.roleNames, normalizedName);
  }

  onRoleChange(role: RoleDto, $event) {
    debugger
    this.checkedRolesMap[role.normalizedName] = $event.target.checked;
  }

  getCheckedRoles(): string[] {
    debugger
    const roles: string[] = [];
    _forEach(this.checkedRolesMap, function (value, key) {
      if (value) {
        roles.push(key);
      }
    });
    return roles;
  }
  getCheckedPermissions(): string[] {
    const permissions: string[] = [];
    _forEach(this.checkedPermissionsMap, function (value, key) {
      if (value) {
        permissions.push(key);
      }
    });
    return permissions;
  }

  save(): void {
    this.saving = true;
    debugger
    this.user.roleNames = this.getCheckedRoles();
    const role = new RoleDto();
    
    role.init(this.role);
    role.grantedPermissions = this.getCheckedPermissions();

    this.user.leaveQuotaId;
    this._userService.update(this.user,).subscribe(
      () => {
        this.notify.info(this.l('SavedSuccessfully'));
        this.bsModalRef.hide();
        this.onSave.emit();
      },
      () => {
        this.saving = false;
      }
    );
  }
  getUserLeaveStatus(){
    this._userService.getUserStatus().subscribe((response)=>{      
       this.leaveStatus= response;     
      })
 }
 isFormEmpty(form: NgForm): boolean {
  return !form.value.name && !form.value.surname && !form.value.userName &&
         !form.value.emailAddress && !form.value.leaveQuotaId  &&
         !form.value.joiningDate && !this.user.isActive &&
         this.roles.every(role => !this.isRoleChecked(role.normalizedName));
}
// setInitialPermissionsStatus(): void {
//   _map(this.permissions, (item) => {
//     this.checkedPermissionsMap[item.name] = this.isPermissionChecked(
//       item.name
//     );
//   });
// }



}
