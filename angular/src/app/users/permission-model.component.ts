import { Component, EventEmitter, Injector, Input, OnInit, Output } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { FlatPermissionDto,  LeaveCateoryAppServicesServiceProxy, PermissionDto,  RoleServiceProxy, UserServiceProxy } from '@shared/service-proxies/service-proxies';
import { forEach as _forEach, includes as _includes, map as _map } from 'lodash-es';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { TreeNode } from 'primeng/api';

@Component({
  selector: 'app-permission-model',
  templateUrl: './permission-model.component.html',
  styleUrls: ['./permission-model.component.css']
})
export class PermissionModelComponent extends AppComponentBase
implements OnInit{
 
  permissions: FlatPermissionDto[];
  
  @Input() id:number;
  @Output() onSave = new EventEmitter<any>();
  // files3: TreeNode[];
  // selectedFiles2: TreeNode[];
  files3: PermissionDto[] = [];
  selectedFiles2: PermissionDto[] = [];
  saving = false;
  constructor(
    injector: Injector,
    public _userService: UserServiceProxy,
    public bsModalRef: BsModalRef,
    private _roleService: RoleServiceProxy,
     private permissionService: LeaveCateoryAppServicesServiceProxy
    
  ) {
    super(injector);
  }
  ngOnInit(): void {
   debugger
  //  this.permissionService.getAllPermissionsPC(this.id).subscribe(permissions => {
  //   debugger
  //   this.files3 = this.convertToTreeNodes(permissions.items);
  //   debugger
  // });
  this.permissionService.getAllPermissionsPC(this.id).subscribe((result) => {
    this.files3 = this.transformPermissions(result.items);
    this.selectedFiles2 = this.getSelectedPermissions(result.items);
   });
  }
  // private convertToTreeNodes(permissions: PermissionDto[], parentName: string = null): TreeNode[] {
  //   return permissions.map(permission => ({
  //     label: permission.displayName,
  //     data: {
  //       name: permission.name,
  //       parentName: parentName
  //     },
  //     children: this.convertToTreeNodes(permission.children || [], permission.name),
  //     expanded: false // Add this property to control the initial state
  //   }));
  // }
  getSelectedPermissions(permissions: PermissionDto[]): PermissionDto[] {
    let selectedPermissions: PermissionDto[] = [];
    permissions.forEach(permission => {
        if (permission.isGranted) {
            selectedPermissions.push(permission);
        }
        if (permission.children) {
            selectedPermissions = selectedPermissions.concat(this.getSelectedPermissions(permission.children));
        }
    });
    return selectedPermissions;
}
transformPermissions(permissions: PermissionDto[]): any[] {
  return permissions.map(permission => ({
      label: permission.displayName,
      data: permission.name,
      children: this.transformPermissions(permission.children),
      selectable: true, // all nodes should be selectable
      key: permission.name // unique identifier for each node
  }));
}
  onNodeSelect(event): void {
    debugger
    if (event.node) {
      event.node.expanded = !event.node.expanded; // Toggle expanded state
    }
  }
save() {
  this.saving = true;
//   const sdsd = new UserDto();
//     sdsd.init(this.sdsd);
   
//     sdsd.grantedPermissions = this.getCheckedPermissions();
 }


}
