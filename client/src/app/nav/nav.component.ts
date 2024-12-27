import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';

@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [FormsModule, BsDropdownModule],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent {
  accountService = inject(AccountService)

  model: any = {};

  login() {
    this.accountService.login(this.model).subscribe({
      next: Response => {
        console.log(Response);
      },
      error: error => console.log(error),
    })
  }

  lougout() {
    this.accountService.logout();
  }
}
