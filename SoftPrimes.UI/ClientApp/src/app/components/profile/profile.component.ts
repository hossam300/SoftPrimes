import { AuthService } from 'src/app/core/_services/auth.service';
import { SettingsCrudsService } from './../../settings/settings-cruds.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, Subject, Subscription } from 'rxjs';
import { AgentDetailsDTO, AgentDTO, AuthTicketDTO, CompanyDTO, RoleDTO } from 'src/app/core/_services/swagger/SwaggerClient.service';
import { setDate, getDate } from 'src/app/core/_utils/date';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  routerSubscription: Subscription;
  viewMode: boolean;
  user: any;
  birthdate;
  roleId: number;

  constructor(
    private settingsCrud: SettingsCrudsService,
    private auth: AuthService
  ) {
    this.user = JSON.parse(localStorage.getItem('user'));
  }

  ngOnInit() {
    this.getUser();
  }

  getUser() {
    this.viewMode = true;
    this.settingsCrud.getUserProfile(this.user.userId).subscribe(user => {
      this.user = user;
      this.birthdate = setDate(user.birthDate);
      if (user.image) {
        this.getBase64Url('image/jpeg', user.image);
      }
      if (user.agentRoles[0]) {
        this.roleId = user.agentRoles[0].roleId;
      }
    });
  }

  editMode() {
    this.viewMode = false;
  }

  updateProfile() {
    this.user.birthDate = getDate(this.birthdate);
    const user = new AgentDetailsDTO({
      id: this.user.id,
      userName: this.user.userName,
      password: this.user.password,
      email: this.user.email,
      jobTitle: this.user.jobTitle,
      mobile: this.user.mobile,
      birthDate: this.user.birthDate,
      fullNameAr: this.user.fullNameAr,
      fullNameEn: this.user.fullNameEn,
      supervisorId: this.user.supervisorId,
      roleId: this.roleId,
      companyId: this.user.companyId,
      isTempPassword: false,
    });
    console.log(this.roleId, user, 'user and roleId');
    this.settingsCrud.updateAgent(this.roleId || 1, user).subscribe(res => {
      if (res) {
        this.viewMode = true;
        const currentUser = new AuthTicketDTO(this.user);
        currentUser.userId = res.id;
        this.auth.setUser(currentUser);
      }
    });
  }

  getBase64Url(fileType: string, url: string) {
    this.user.image = `data:${fileType};base64,${url}`;
  }

  readURL(event) {
    console.log(event, 'read url from event');
    if (event.target.files && event.target.files[0]) {
        const reader = new FileReader();
        reader.onload = function(e) {
          // document.getElementById('imagePreview').style.backgroundImage =  'url(' + e.target.result + ')';
          document.getElementById('imagePreview').classList.add('d-none');
          document.getElementById('imagePreview').classList.remove('d-none');
        };
        reader.readAsDataURL(event.target.files[0]);
    }
    const fileType = event.target.files[0].type;
    const formData = new FormData();
    formData.append('upload', event.target.files[0]);
    this.settingsCrud.addUserImage(this.user.id, formData).subscribe((res: any) => {
      console.log(res, 'image uploaded');
      this.getBase64Url(fileType, res.profileImage);
    });
}

}
