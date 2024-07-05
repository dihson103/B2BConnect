import AppSideBarButton from '@/app/admin/_components/side-button'
import { CalendarCheck, Home, LineChart, Package, Package2, Settings, Users2 } from 'lucide-react'
import Link from 'next/link'

export default function AppSideBar() {
  return (
    <aside className='fixed inset-y-0 left-0 z-10 hidden w-14 flex-col border-r bg-background sm:flex'>
      <nav className='flex flex-col items-center gap-4 px-2 py-4'>
        <Link
          href='#'
          className='group flex h-9 w-9 shrink-0 items-center justify-center gap-2 rounded-full bg-primary text-lg font-semibold text-primary-foreground md:h-8 md:w-8 md:text-base'
        >
          <Package2 className='h-4 w-4 transition-all group-hover:scale-110' />
          <span className='sr-only'>Acme Inc</span>
        </Link>

        <AppSideBarButton url='/admin/dashboard' displayValue='Trang thống kê'>
          <Home className='h-5 w-5' />
        </AppSideBarButton>
        <AppSideBarButton url='/admin/users' displayValue='Quản lý người dùng'>
          <Users2 className='h-5 w-5' />
        </AppSideBarButton>
        <AppSideBarButton url='/admin/events' displayValue='Quản lý sự kiện'>
          <CalendarCheck className='h-5 w-5' />
        </AppSideBarButton>
        <AppSideBarButton url='/admin/products' displayValue='Products'>
          <Package className='h-5 w-5' />
        </AppSideBarButton>
        <AppSideBarButton url='/admin/anlytics' displayValue='Analytics'>
          <LineChart className='h-5 w-5' />
        </AppSideBarButton>
      </nav>
      <nav className='mt-auto flex flex-col items-center gap-4 px-2 py-4'>
        <AppSideBarButton url='/admin/anlytics' displayValue='Settings'>
          <Settings className='h-5 w-5' />
        </AppSideBarButton>
      </nav>
    </aside>
  )
}
