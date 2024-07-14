import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { CreateUserDto, FlatPermissionDto, GetRoleForEditOutput, PermissionDto, RoleEditDto, RoleServiceProxy, UserDto, UserServiceProxy } from '@shared/service-proxies/service-proxies';
import { forEach as _forEach, includes as _includes, map as _map } from 'lodash-es';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-permission-model',
  templateUrl: './permission-model.component.html',
  styleUrls: ['./permission-model.component.css']
})
export class PermissionModelComponent extends AppComponentBase
implements OnInit{
  grantedPermissionNames: string[];
  checkedPermissionsMap: { [key: string]: boolean } = {};
  permissions: FlatPermissionDto[];
  sdsd = new CreateUserDto();
  saving = false;
  role = new RoleEditDto();
  user = new UserDto();
  id: number;
  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
    public _userService: UserServiceProxy,
    public bsModalRef: BsModalRef,
    private _roleService: RoleServiceProxy

  ) {
    super(injector);
  }
  ngOnInit(): void {
   debugger
    this.setInitialPermissionsStatus();
    // this._roleService
    //   .getRoleForEdit(this.id)
    //   .subscribe((result: GetRoleForEditOutput) => {
    //     this.role = result.role;
    //     this.permissions = result.permissions;
    //     this.grantedPermissionNames = result.grantedPermissionNames;
    //     this.setInitialPermissionsStatus();
    //   });
  }
save() {
  const sdsd = new UserDto();
    sdsd.init(this.sdsd);
   
    sdsd.grantedPermissions = this.getCheckedPermissions();
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
isPermissionChecked(permissionName: string): boolean {
  return _includes(this.grantedPermissionNames, permissionName);
}

onPermissionChange(permission: PermissionDto, $event) {
  this.checkedPermissionsMap[permission.name] = $event.target.checked;
}
setInitialPermissionsStatus(): void {
  _map(this.permissions, (item) => {
    this.checkedPermissionsMap[item.name] = this.isPermissionChecked(
      item.name
    );
  });
}
}
