'use client'

import {
  Breadcrumb,
  BreadcrumbItem,
  BreadcrumbLink,
  BreadcrumbList,
  BreadcrumbPage,
  BreadcrumbSeparator
} from '@/components/ui/breadcrumb'
import { BreadcrumbValue, GetMapPathName } from '@/constants/utils.constants'
import Link from 'next/link'
import { usePathname } from 'next/navigation'

export default function AppBreadcrumb() {
  const pathname = usePathname()
  const pathMap = GetMapPathName(pathname)
  return (
    <Breadcrumb className='hidden md:flex'>
      <BreadcrumbList>
        <BreadcrumbItem>
          <BreadcrumbLink asChild>
            <Link href='/admin/dashboard'>Quản lý</Link>
          </BreadcrumbLink>
        </BreadcrumbItem>
        <BreadcrumbSeparator />
        <BreadcrumbItem>
          <BreadcrumbLink asChild>
            <Link href={BreadcrumbValue.get(pathMap ?? '')?.url ?? '#'}>
              {BreadcrumbValue.get(pathMap ?? '')?.display}
            </Link>
          </BreadcrumbLink>
        </BreadcrumbItem>
        <BreadcrumbSeparator />
        <BreadcrumbItem>
          <BreadcrumbPage>{BreadcrumbValue.get(pathMap ?? '')?.value}</BreadcrumbPage>
        </BreadcrumbItem>
      </BreadcrumbList>
    </Breadcrumb>
  )
}
