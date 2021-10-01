import { AuthService } from 'src/app/core/_services/auth.service';
import { SettingsCrudsService } from './../../settings/settings-cruds.service';
import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { AgentDetailsDTO, AuthTicketDTO } from 'src/app/core/_services/swagger/SwaggerClient.service';
import { fixDateTimePickers } from 'src/app/core/_utils/date';
import { LoaderService } from 'src/app/core/_services/loader.service';

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
    private auth: AuthService,
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
    fixDateTimePickers();
  }

  updateProfile() {
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
    // this.loader.addLoader();
    this.settingsCrud.updateAgent(this.roleId || 1, user).subscribe(res => {
      // this.loader.removeLoader();;
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
      this.getBase64Url(fileType, res.profileImage);
    });
}

}
