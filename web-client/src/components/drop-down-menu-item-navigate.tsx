'use client'

import { DropdownMenuItem } from '@/components/ui/dropdown-menu'
import { useRouter } from 'next/navigation'

type Props = {
  url: string
  displayName: string
}

export default function DropdownMenuItemNavigate({ url, displayName }: Props) {
  const router = useRouter()
  const handlerClick = () => {
    router.push(url)
  }

  return <DropdownMenuItem onClick={handlerClick}>{displayName}</DropdownMenuItem>
}
