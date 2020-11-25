import { Component, OnInit } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { AdminService } from 'src/app/services/admin.service';
import { AlertifyService } from 'src/app/services/alertify.service';
import { User } from 'src/app/_models/User';
import { RolesModalComponent } from '../roles-modal/roles-modal.component';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.css']
})
export class UserManagementComponent implements OnInit {
    users: User[];
    bsModalRef: BsModalRef;
  
    constructor(private adminService: AdminService, private alertifyService: AlertifyService, private modalService: BsModalService) { }
  
    ngOnInit() {
      this.getUsersWithRoles();
    }
  
    getUsersWithRoles() {
      this.adminService.getUsersWithRoles().subscribe((users: User[]) => {
        this.users = users;
      }, error => {
        this.alertifyService.error(error);
      });
    }
  
    editRolesModal(user: User) {
      const initialState = {
        user,
        roles: this.getRolesArray(user)
      };
      this.bsModalRef = this.modalService.show(RolesModalComponent, {initialState});
      this.bsModalRef.content.updateSelectedRoles.subscribe((values) => {
        const rolesToUpdate = {
          roleNames: [...values.filter(el => el.checked === true).map(el => el.name)]
        };
        if (rolesToUpdate) {
          this.adminService.updateUserRoles(user, rolesToUpdate).subscribe(() => {
            user.roles = [...rolesToUpdate.roleNames];
          }, error => {
            this.alertifyService.error(error);
          });
        }
      });
    }
  
    private getRolesArray(user) {
      const roles = [];
      const userRoles = user.roles;
      const availableRoles: any[] = [
        {name: 'Admin', value: 'Admin'},
        {name: 'Member', value: 'Member'}
      ];
  
      // tslint:disable-next-line: prefer-for-of
      for (let i = 0; i < availableRoles.length; i++) {
        let isMatch = false;
        // tslint:disable-next-line: prefer-for-of
        for (let j = 0; j < userRoles.length; j++) {
          if (availableRoles[i].name === userRoles[j]) {
            isMatch = true;
            availableRoles[i].checked = true;
            roles.push(availableRoles[i]);
            break;
          }
        }
        if (!isMatch) {
          availableRoles[i].checked = false;
          roles.push(availableRoles[i]);
        }
      }
      return roles;
    }

}
