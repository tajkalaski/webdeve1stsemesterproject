import { TranslateService } from '@ngx-translate/core';
import { LanguageService } from './../services/language.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AuthService } from './../services/auth.service';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { ApplicationUser } from '../models/application-user';
import { Subscription, interval, of } from 'rxjs';
import { Title } from '@angular/platform-browser';
import { RoleService } from '../services/role.service';
import { switchMap, map } from 'rxjs/operators';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'manage-profile',
  templateUrl: './manage-profile.component.html',
  styleUrls: ['./manage-profile.component.scss']
})
export class ManageProfileComponent implements OnInit, OnDestroy {
  user: ApplicationUser;
  companyId: string;
  profileForm: FormGroup;
  subscription: Subscription;
  updateSubscription: Subscription;
  roles: any[] = []; // TO DO: Create roles custom type
  languages: any[] = []; // TO DO: Create languages custom type

  constructor(
    private auth: AuthService,
    private titleService: Title,
    private accountService: AccountService,
    private languageService: LanguageService,
    private roleService: RoleService,
    private translateService: TranslateService
  ) { }

  updateProfile() {
    if (this.profileForm.invalid) {
      return;
    }
    this.profileForm.value.language = this.language.value.id;
    this.profileForm.value.role = this.roles.find(roles => roles.name === this.role.value).id;
    this.updateSubscription = this.accountService.update(this.profileForm.value).subscribe((user: ApplicationUser) => {
      this.user = user;
      this.translateService.use(this.user.language);
    });
  }

  changePassword() {

  }

  deleteUser() {

  }

  cancel() {
    if (!confirm('Are you sure you want to cancel the changes?')) {
      return;
    }
    this.firstName.setValue(this.user.firstName);
    this.lastName.setValue(this.user.lastName);
    this.phoneNumber.setValue(this.user.phoneNumber);
    this.email.setValue(this.user.email);
    this.role.setValue(this.user.role);
    this.language.setValue(this.languages.find(language => language.name === this.user.language));
  }

  ngOnInit() {
    this.titleService.setTitle('Respaunce | Your profile');

    // let interval$ = interval(29*1000);
    // interval$.pipe(switchMap(val => {this.auth.user$.subscribe(); return of(val)})).subscribe(val => console.log(val))

    this.profileForm = new FormGroup({
      firstName: new FormControl('', Validators.required),
      lastName: new FormControl('', Validators.required),
      phoneNumber: new FormControl('', Validators.required),
      email: new FormControl('', Validators.required),
      language: new FormControl('', Validators.required),
      role: new FormControl('', Validators.required),
    });

    this.subscription = this.auth.user$
      .pipe(
        switchMap((user: ApplicationUser) => {
          this.user = user;
          this.companyId = user.companyId;
          this.profileForm.addControl('companyId', new FormControl(this.companyId, Validators.required));
          this.firstName.setValue(user.firstName);
          this.lastName.setValue(user.lastName);
          this.phoneNumber.setValue(user.phoneNumber);
          this.email.setValue(user.email);
          this.role.setValue(user.role);
          return this.languageService.getAll();
        }),
        switchMap(languages => {
          this.languages = languages;
          this.language.setValue(this.languages.find(language => language.name === this.user.language));
          return this.roleService.getAll();
        }),
        map(roles => {
          this.roles = roles;
        })
      )
      .subscribe();
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  get firstName() {
    return this.profileForm.get('firstName') as FormControl;
  }

  get lastName() {
    return this.profileForm.get('lastName') as FormControl;
  }

  get phoneNumber() {
    return this.profileForm.get('phoneNumber') as FormControl;
  }

  get email() {
    return this.profileForm.get('email') as FormControl;
  }

  get language() {
    return this.profileForm.get('language') as FormControl;
  }

  get role() {
    return this.profileForm.get('role') as FormControl;
  }
}
