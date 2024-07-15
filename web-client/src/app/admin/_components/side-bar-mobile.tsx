import {
  Bell,
  BriefcaseBusiness,
  CalendarCheck,
  Home,
  LineChart,
  Package,
  Package2,
  PanelLeft,
  Users2
} from 'lucide-react'
import Link from 'next/link'
import { Sheet, SheetContent, SheetTrigger } from '@/components/ui/sheet'
import { Button } from '@/components/ui/button'
import AppSideMobileLink from '@/app/admin/_components/side-mobile-link'

export default function AppSideBarMobile() {
  return (
    <Sheet>
      <SheetTrigger asChild>
        <Button size='icon' variant='outline' className='sm:hidden'>
          <PanelLeft className='h-5 w-5' />
          <span className='sr-only'>Toggle Menu</span>
        </Button>
      </SheetTrigger>
      <SheetContent side='left' className='sm:max-w-xs'>
        <nav className='grid gap-6 text-lg font-medium'>
          <Link
            href='#'
            className='group flex h-10 w-10 shrink-0 items-center justify-center gap-2 rounded-full bg-primary text-lg font-semibold text-primary-foreground md:text-base'
          >
            <Package2 className='h-5 w-5 transition-all group-hover:scale-110' />
            <span className='sr-only'>Acme Inc</span>
          </Link>
          <AppSideMobileLink url='/admin' displayValue='Trang thống kê'>
            <Home className='h-5 w-5' />
          </AppSideMobileLink>
          <AppSideMobileLink url='/admin/businesses' displayValue='Quản lý doanh nghiệp'>
            <BriefcaseBusiness className='h-5 w-5' />
          </AppSideMobileLink>
          <AppSideMobileLink url='/admin/users' displayValue='Quản lý người dùng'>
            <Users2 className='h-5 w-5' />
          </AppSideMobileLink>
          <AppSideMobileLink url='/admin/events' displayValue='Quản lý sự kiện'>
            <CalendarCheck className='h-5 w-5' />
          </AppSideMobileLink>
          <Link
            href='/admin/products'
            className='flex items-center gap-4 px-2.5 text-muted-foreground hover:text-foreground'
          >
            <Package className='h-5 w-5' />
            Products
          </Link>

          <Link href='#' className='flex items-center gap-4 px-2.5 text-muted-foreground hover:text-foreground'>
            <LineChart className='h-5 w-5' />
            Settings
          </Link>
        </nav>
      </SheetContent>
    </Sheet>
  )
}
