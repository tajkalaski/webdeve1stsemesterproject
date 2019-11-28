import { TranslateService } from '@ngx-translate/core';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from './../services/auth.service';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription, timer } from 'rxjs';
import { ApplicationUser } from '../models/application-user';
import { map, switchMap } from 'rxjs/operators';

@Component({
  selector: 'nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent implements OnInit, OnDestroy {
  user: ApplicationUser;
  subscription: Subscription;
  analysisMenuItem = { name: 'analysis', selected: false };
  complianceMenuItem = { name: 'compliance', selected: false };
  focusAreasMenuItem = { name: 'focusareas', selected: false };
  suppliersMenuItem = { name: 'suppliers', selected: false };
  reportsMenuItem = { name: 'reports', selected: false };
  dataMenuItem = { name: 'data', selected: false };
  adminMenuItem = { name: 'admin', selected: false };
  menuItems: { name: string, selected: boolean }[] = [
    this.analysisMenuItem,
    this.complianceMenuItem,
    this.focusAreasMenuItem,
    this.suppliersMenuItem,
    this.reportsMenuItem,
    this.dataMenuItem,
    this.adminMenuItem,
  ];
  currentMenuItem: string;
  showSubMenu = false;
  urlSubscription: Subscription;

  constructor(
    private auth: AuthService,
    private translate: TranslateService,
    private route: ActivatedRoute
  ) { }

  focus(id?: string) {
    this.menuItems.forEach(menuItem => {
      menuItem.selected = false;
    });
    if (id === 'manage-assessments') { id = 'admin'; }
    if (id === 'manage-company') { id = 'admin'; }
    if (id === 'manage-users') { id = 'admin'; }
    if (id === 'manage-documents') { id = 'admin'; }
    if (id === 'manage-profile') { id = 'admin'; }
    if (id) { this.menuItems.find(menuItem => menuItem.name === id).selected = true; }
  }

  toggleSubMenu() {
    this.showSubMenu = !this.showSubMenu;
  }

  logOut() {
    this.auth.logout();
  }

  changeLanguage(lang: string) {
    this.translate.use(lang);
  }

  ngOnInit() {
    this.subscription = this.auth.user$
      .subscribe((user: ApplicationUser) => {
        this.user = user;
      });

    const interval$ = timer(150);

    this.urlSubscription = interval$
      .pipe(
        switchMap(() => {
          return this.route.pathFromRoot[0].firstChild.url
            .pipe(
              map((url: any[]) => {
                if (url.length !== 0) { this.currentMenuItem = url[0].path; }
                if (this.currentMenuItem) { this.focus(this.currentMenuItem); }
              })
            );
          }
        )
      )
      .subscribe();
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
    this.urlSubscription.unsubscribe();
  }
}
