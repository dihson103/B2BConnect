import { Bell, Home, LineChart, Package, Package2, PanelLeft, ShoppingCart, Users2 } from 'lucide-react'
import Link from 'next/link'
import { Sheet, SheetContent, SheetTrigger } from '@/components/ui/sheet'
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger
} from '@/components/ui/dropdown-menu'
import { Button } from '@/components/ui/button'
import Image from 'next/image'
import { ModeToggle } from '@/components/mode-toggle'
import AppSideBar from '@/app/admin/components/side-bar'
import AppBreadcrumb from '@/app/admin/components/breadcrumb'

export default async function AdminLayout({
  children
}: Readonly<{
  children: React.ReactNode
}>) {
  return (
    <div className='flex min-h-screen w-full flex-col bg-muted/40'>
      <AppSideBar />
      <div className='flex flex-col sm:gap-4 sm:py-4 sm:pl-14'>
        <header className='sticky top-0 z-30 flex h-14 items-center gap-4 border-b bg-background px-4 sm:static sm:h-auto sm:border-0 sm:bg-transparent sm:px-6'>
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
                <Link
                  href='/admin'
                  className='flex items-center gap-4 px-2.5 text-muted-foreground hover:text-foreground'
                >
                  <Home className='h-5 w-5' />
                  Dashboard
                </Link>
                <Link
                  href='/admin/orders'
                  className='flex items-center gap-4 px-2.5 text-muted-foreground hover:text-foreground'
                >
                  <ShoppingCart className='h-5 w-5' />
                  Orders
                </Link>
                <Link href='/admin/products' className='flex items-center gap-4 px-2.5 text-foreground'>
                  <Package className='h-5 w-5' />
                  Products
                </Link>
                <Link
                  href='/admin/users'
                  className='flex items-center gap-4 px-2.5 text-muted-foreground hover:text-foreground'
                >
                  <Users2 className='h-5 w-5' />
                  Users
                </Link>
                <Link href='#' className='flex items-center gap-4 px-2.5 text-muted-foreground hover:text-foreground'>
                  <LineChart className='h-5 w-5' />
                  Settings
                </Link>
              </nav>
            </SheetContent>
          </Sheet>
          <AppBreadcrumb />

          <div className='ml-auto flex items-center gap-4'>
            <h3 className=''>Xin chào dihson103</h3>
            <Button variant='outline' size='icon' className='h-8 w-8'>
              <Bell className='h-4 w-4' />
              <span className='sr-only'>Toggle notifications</span>
            </Button>
          </div>
          <ModeToggle />
          <DropdownMenu>
            <DropdownMenuTrigger asChild>
              <Button variant='outline' size='icon' className='overflow-hidden rounded-full'>
                <Image
                  src='/placeholder-user.jpg'
                  width={36}
                  height={36}
                  alt='Avatar'
                  className='overflow-hidden rounded-full'
                />
              </Button>
            </DropdownMenuTrigger>
            <DropdownMenuContent align='end'>
              <DropdownMenuLabel>My Account</DropdownMenuLabel>
              <DropdownMenuSeparator />
              <DropdownMenuItem>Settings</DropdownMenuItem>
              <DropdownMenuItem>Support</DropdownMenuItem>
              <DropdownMenuSeparator />
              <DropdownMenuItem>Logout</DropdownMenuItem>
            </DropdownMenuContent>
          </DropdownMenu>
        </header>
        <main className='grid flex-1 items-start gap-4 p-4 sm:px-6 sm:py-0 md:gap-8'>{children}</main>
      </div>
    </div>
  )
}
