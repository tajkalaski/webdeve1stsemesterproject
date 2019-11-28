import { DataEntryComponent } from './data-entry/data-entry.component';
import { ManageSupplierCertificateComponent } from './manage-supplier-certificate/manage-supplier-certificate.component';
import { ManageSupplierCertificatesComponent } from './manage-supplier-certificates/manage-supplier-certificates.component';
import { ManageComplianceComponent } from './manage-compliance/manage-compliance.component';
import { CreateSupplierComponent } from './create-supplier/create-supplier.component';
import { AdminAuthGuard } from './services/guards/admin-auth-guard.service';
import { ManageProfileComponent } from './manage-profile/manage-profile.component';
import { ManageCompanyComponent } from './manage-company/manage-company.component';
import { CreateUserComponent } from './create-user/create-user.component';
import { ManageUserComponent } from './manage-user/manage-user.component';
import { CreateAssessmentComponent } from './create-assessment/create-assessment.component';
import { ManageAssessmentsComponent } from './manage-assessments/manage-assessments.component';
import { AnalysisComponent } from './analysis/analysis.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginFormComponent } from './login-form/login-form.component';
import { AuthGuard } from './services/guards/auth-guard.service';
import { NonAuthGuard } from './services/guards/non-auth-guard.service';
import { UsersComponent } from './users/users.component';
import { ManageAssessmentComponent } from './manage-assessment/manage-assessment.component';
import { ManageReportsComponent } from './manage-reports/manage-reports.component';
import { ManageSuppliersComponent } from './manage-suppliers/manage-suppliers.component';
import { ManageFocusAreasComponent } from './manage-focus-areas/manage-focus-areas.component';
import { ManageDocumentsComponent } from './manage-documents/manage-documents.component';
import { NonSupplierAuthGuard } from './services/guards/non-supplier-auth.guard';
import { ManageSupplierComponent } from './manage-supplier/manage-supplier.component';
import { ManageCertificateComponent } from './manage-certificate/manage-certificate.component';
import { ManageCertificatesComponent } from './manage-certificates/manage-certificates.component';

const routes: Routes = [
  {
    path: '',
    component: DashboardComponent,
    canActivate: [AuthGuard, NonSupplierAuthGuard]
  },
  {
    path: 'suppliers/create',
    component: CreateSupplierComponent,
    canActivate: [AuthGuard, NonSupplierAuthGuard]
  },
  {
    path: 'suppliers/:id/certificates/:certificateId',
    component: ManageSupplierCertificateComponent,
    canActivate: [AuthGuard, NonSupplierAuthGuard]
  },
  {
    path: 'suppliers/:id/certificates',
    component: ManageSupplierCertificatesComponent,
    canActivate: [AuthGuard, NonSupplierAuthGuard]
  },
  {
    path: 'suppliers/:id',
    component: ManageSupplierComponent,
    canActivate: [AuthGuard, NonSupplierAuthGuard]
  },
  {
    path: 'suppliers',
    component: ManageSuppliersComponent,
    canActivate: [AuthGuard, NonSupplierAuthGuard]
  },
  {
    path: 'compliance/certificates/:id',
    component: ManageCertificateComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'compliance/certificates',
    component: ManageCertificatesComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'compliance',
    component: ManageComplianceComponent,
    canActivate: [AuthGuard, NonSupplierAuthGuard]
  },
  {
    path: 'focusareas',
    component: ManageFocusAreasComponent,
    canActivate: [AuthGuard, NonSupplierAuthGuard]
  },
  {
    path: 'analysis',
    component: AnalysisComponent,
    canActivate: [AuthGuard, NonSupplierAuthGuard]
  },
  {
    path: 'reports',
    component: ManageReportsComponent,
    canActivate: [AuthGuard, NonSupplierAuthGuard]
  },
  {
    path: 'manage-assessments/create',
    component: CreateAssessmentComponent,
    canActivate: [AuthGuard, NonSupplierAuthGuard]
  },
  {
    path: 'manage-assessments/:id',
    component: ManageAssessmentComponent,
    canActivate: [AuthGuard, NonSupplierAuthGuard]
  },
  {
    path: 'manage-assessments',
    component: ManageAssessmentsComponent,
    canActivate: [AuthGuard, NonSupplierAuthGuard]
  },
  {
    path: 'manage-company',
    component: ManageCompanyComponent,
    canActivate: [AuthGuard, AdminAuthGuard]
  },
  {
    path: 'manage-users/create',
    component: CreateUserComponent,
    canActivate: [AuthGuard, AdminAuthGuard]
  },
  {
    path: 'manage-users/:email',
    component: ManageUserComponent,
    canActivate: [AuthGuard, AdminAuthGuard]
  },
  {
    path: 'manage-users',
    component: UsersComponent,
    canActivate: [AuthGuard, AdminAuthGuard]
  },
  {
    path: 'manage-profile',
    component: ManageProfileComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'manage-documents',
    component: ManageDocumentsComponent,
    canActivate: [AuthGuard, NonSupplierAuthGuard]
  },
  {
    path: 'login',
    component: LoginFormComponent,
    canActivate: [NonAuthGuard]
  },
  {
    path: 'data',
    component: DataEntryComponent,
    canActivate: [AuthGuard, NonSupplierAuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
