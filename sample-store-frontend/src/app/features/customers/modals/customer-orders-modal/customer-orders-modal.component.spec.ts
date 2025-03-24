import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CustomerOrdersModalComponent } from './customer-orders-modal.component';


describe('CustomerOrdersModalComponent', () => {
  let component: CustomerOrdersModalComponent;
  let fixture: ComponentFixture<CustomerOrdersModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CustomerOrdersModalComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CustomerOrdersModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
