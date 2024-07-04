'use client'

import AppToolTip from '@/components/tooltip'
import Link from 'next/link'
import { usePathname } from 'next/navigation'
import React, { ReactNode } from 'react'

type Props = {
  displayValue: string
  url: string
  children: ReactNode
}

export default function AppSideBarButton({ displayValue, url, children }: Props) {
  const pathname = usePathname()

  return (
    <AppToolTip displayValue={displayValue}>
      <Link
        href={url}
        className={`flex h-9 w-9 items-center justify-center rounded-lg ${
          pathname === url ? 'bg-accent text-accent-foreground' : 'text-muted-foreground'
        } transition-colors hover:text-foreground md:h-8 md:w-8`}
      >
        {children}
        <span className='sr-only'>{displayValue}</span>
      </Link>
    </AppToolTip>
  )
}
