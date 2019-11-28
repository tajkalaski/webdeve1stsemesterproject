import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AdminAuthGuard } from './services/guards/admin-auth-guard.service';
import { RoleService } from './services/role.service';
import { NonAuthGuard } from './services/guards/non-auth-guard.service';
import { QuestionService } from './services/question.service';
import { LegalFormService } from './services/legal-form.service';
import { AssessmentService } from './services/assessment.service';
import { CompanyService } from './services/company.service';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { UserService } from './services/user.service';
import { AuthGuard } from './services/guards/auth-guard.service';
import { HttpClient, HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ErrorHandler } from '@angular/core';
import { NgbModule, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { FooterMenuComponent } from './footer-menu/footer-menu.component';
import { LoginFormComponent } from './login-form/login-form.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { AnalysisComponent } from './analysis/analysis.component';
import { UsersComponent } from './users/users.component';
import { GuideComponent } from './guide/guide.component';
import { ManageAssessmentsComponent } from './manage-assessments/manage-assessments.component';
import { CreateAssessmentComponent } from './create-assessment/create-assessment.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ManageUserComponent } from './manage-user/manage-user.component';
import { CreateUserComponent } from './create-user/create-user.component';
import { ManageAssessmentComponent } from './manage-assessment/manage-assessment.component';
import { TypeOfOwnershipService } from './services/type-of-ownership.service';
import { ManageProfileComponent } from './manage-profile/manage-profile.component';
import { ManageCompanyComponent } from './manage-company/manage-company.component';
import { UnauthorizedErrorHandling } from './error-handling/unauthorized-error-handling';
import { ManageReportsComponent } from './manage-reports/manage-reports.component';
import { ManageSuppliersComponent } from './manage-suppliers/manage-suppliers.component';
import { ManageFocusAreasComponent } from './manage-focus-areas/manage-focus-areas.component';
import { ManageDocumentsComponent } from './manage-documents/manage-documents.component';
import { UtilityToolsComponent } from './utility-tools/utility-tools.component';
import { CreateSupplierComponent } from './create-supplier/create-supplier.component';
import { ManageSupplierComponent } from './manage-supplier/manage-supplier.component';
import { MasterDataRequestComponent } from './master-data-request/master-data-request.component';
import { ManageComplianceComponent } from './manage-compliance/manage-compliance.component';
import { ManageSupplierCertificatesComponent } from './manage-supplier-certificates/manage-supplier-certificates.component';
import { ManageCertificateComponent } from './manage-certificate/manage-certificate.component';
import { AddCertificateComponent } from './add-certificate/add-certificate.component';
import { ManageSupplierCertificateComponent } from './manage-supplier-certificate/manage-supplier-certificate.component';
import { ManageCertificatesComponent } from './manage-certificates/manage-certificates.component';
import { ContentIntroComponent } from './content-intro/content-intro.component';
import { environment } from 'src/environments/environment';
import { APIInterceptor } from './services/api-interceptor.service';
import { DataEntryComponent } from './data-entry/data-entry.component';
import { AddDataEntryComponent } from './add-data-entry/add-data-entry.component';
import { EditDataEntryComponent } from './edit-data-entry/edit-data-entry.component';

export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http);
}

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    FooterMenuComponent,
    LoginFormComponent,
    DashboardComponent,
    AnalysisComponent,
    UsersComponent,
    GuideComponent,
    ManageAssessmentsComponent,
    ManageAssessmentComponent,
    CreateAssessmentComponent,
    ManageUserComponent,
    CreateUserComponent,
    ManageProfileComponent,
    ManageCompanyComponent,
    ManageReportsComponent,
    ManageSuppliersComponent,
    ManageFocusAreasComponent,
    ManageDocumentsComponent,
    UtilityToolsComponent,
    CreateSupplierComponent,
    ManageSupplierComponent,
    MasterDataRequestComponent,
    ManageComplianceComponent,
    ManageSupplierCertificatesComponent,
    ManageCertificateComponent,
    AddCertificateComponent,
    ManageSupplierCertificateComponent,
    ManageCertificatesComponent,
    ContentIntroComponent,
    DataEntryComponent,
    AddDataEntryComponent,
    EditDataEntryComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    }),
    NgbModule,

  ],
  providers: [
    HttpClient,
    NgbActiveModal,
    UserService,
    AuthGuard,
    AdminAuthGuard,
    NonAuthGuard,
    UserService,
    CompanyService,
    QuestionService,
    AssessmentService,
    LegalFormService,
    TypeOfOwnershipService,
    RoleService,
    {provide: ErrorHandler, useClass: UnauthorizedErrorHandling},
    // {provide: 'BASE_API', useValue: environment.apiRoot},
    {provide: HTTP_INTERCEPTORS, useClass: APIInterceptor, multi: true}
  ],
  bootstrap: [AppComponent],
  entryComponents: [
    MasterDataRequestComponent,
    AddCertificateComponent,
    AddDataEntryComponent,
    EditDataEntryComponent
  ],
})
export class AppModule { }
