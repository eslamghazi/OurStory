import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FooterComponent } from './footer/footer.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { SectionHeaderComponent } from './section-header/section-header.component';
import { BreadcrumbModule } from 'xng-breadcrumb';

@NgModule({
  declarations: [NavBarComponent, SectionHeaderComponent, FooterComponent],
  imports: [CommonModule, BreadcrumbModule],
  exports: [NavBarComponent, SectionHeaderComponent, FooterComponent],
})
export class LayoutModule {}
